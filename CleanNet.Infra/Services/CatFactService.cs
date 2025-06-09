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

    public CatFactService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _url = configuration["CatFactApi:Url"]!;
    }
    public async Task<(string fact, int length)> GetCatFactAsync()
    {
        var res = await _httpClient.GetStringAsync(_url);
        using var doc = JsonDocument.Parse(res);
        string fact = doc.RootElement.GetProperty("fact").GetString();
        int length = doc.RootElement.GetProperty("length").GetInt32();
        return (fact, length);
    }
}
