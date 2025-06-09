using System.Diagnostics;
using OpenTelemetry.Trace;

namespace CleanNet.Infra.Common;

public static class TracingHelper
{
    private static readonly ActivitySource _activitySource = new("CleanNet");

    public static Activity? StartActivity(string name, ActivityKind kind = ActivityKind.Internal)
    {
        return _activitySource.StartActivity(name, kind);
    }

    public static void SetError(this Activity? activity, Exception ex)
    {
        if (activity == null) return;
        activity.SetStatus(ActivityStatusCode.Error, ex.Message);
        activity.RecordException(ex);
    }
}