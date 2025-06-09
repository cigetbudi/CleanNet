using CleanNet.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanNet.Infra.Services;

public class CatFactService : ICatFactService
{
     private readonly HttpClient _httpClient;
    private readonly string _url;
    private readonly IAppLogger<CatFactService> _logger;

    public CatFactService(HttpClient httpClient, IConfiguration configuration, IAppLogger<CatFactService> logger)
    {
        _httpClient = httpClient;
        _url = configuration["CatFactApi:Url"]!;
        _logger = logger;
    }

    public async Task<(string fact, int length)> GetCatFactAsync()
    {
        try
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            var res = await _httpClient.GetStringAsync(_url);
            using var doc = JsonDocument.Parse(res);
            string fact = doc.RootElement.GetProperty("fact").GetString()!;
            int length = doc.RootElement.GetProperty("length").GetInt32();
            return (fact, length);
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            // Timeout
            _logger.LogError(ex, "Timeout saat memanggil API CatFact");
            throw new TimeoutException("Permintaan ke API CatFact melebihi batas waktu.");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request gagal ke API CatFact");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kesalahan umum saat ambil data dari API CatFact");
            throw;
        }
    }
}
