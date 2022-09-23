# Operations (formerly know as Stackdriver)

Suite of tools that allows you to monitor, troubleshoot and improve application performance on a Google Cloud environment.

* Collects metrics, logs and traces across GCP and your apps
* Has buil-in out-of-the-box dashboards and views to monitor systems
* Setup alerts and notification rules

The suite contains the following Modules (which are detailed below):

1. Cloud Logging
2. Cloud Monitoring
3. Error Reporting
4. Application Performance Management
   1. Cloud Debugger
   2. Cloud Trace
   3. Cloud Profiler



## Error Reporting
Error Reporting counts, analyzes, and aggregates the crashes in your running cloud services. A centralized error management interface displays the results with sorting and filtering capabilities. A dedicated view shows the error details: time chart, occurrences, affected user count, first- and last-seen dates and a cleaned exception stack trace. Opt in to receive email and mobile alerts on new errors.

App Engine and Cloud Run supports Error Reporting by default, all exceptions are written to `stderr`, `stdout` or other logs that include a stack trace in any [supported language](https://cloud.google.com/error-reporting/docs/setup#setup-by-language).

Cloud Functions is configured to use Error Reporting automatically. Unhandled JavaScript exceptions will appear in Logging and be processed by Error Reporting without needing to use the Error Reporting library for Node.js.

## Application Performance Management (APM)
Is a suite of developer tools to monitoring you app at runtime so you can understand how they behave in production.

### Cloud Debugger
Cloud Debugger is a feature of Google Cloud Platform that lets you inspect the state of an application, at any code location, without stopping or slowing down the running app. Cloud Debugger makes it easier to view the application state without adding logging statements.
You can use Cloud Debugger with any deployment of your application, including test, development, and production. The debugger adds less than 10ms to the request latency only when the application state is captured. In most cases, this is not noticeable by users.
### Cloud Trace
Distributed tracing system that collects latency data from you applications and displays it in the GCP Console Dashboards.

Allows you to find performance bottlenecks by inspecting detail latency information for a single request or view aggregated latency for an entire application.

All the data can be view via the Analysis Report feature, which allows you to compare over time and/or versions of you application.

App Engine Environments have buil-in support for Cloud Trace and automatically capture and send trace latency data to Cloud Trace. All others must configure tracing instrumentation by either using `OpenTelemetry`, `OpenCensus` or the `Clout Trace API`.

### Cloud Profiler
Cloud Profiler is a statistical, low-overhead profiler that continuously gathers CPU usage and memory-allocation information from your production applications. It attributes that information to the application's source code, helping you identify the parts of the application consuming the most resources, and otherwise illuminating the performance characteristics of the code.
