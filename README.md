# WcfInventorySample

A small but complete sample: a WPF/MVVM inventory-tracking app on SQL Server,
with a WCF service handling all inventory logic and database work, plus an
equivalent ASP.NET web client using the SAME service — one backend serving a
desktop front end and a web front end.

**Stack (Path A):** .NET Framework 4.8, WCF, Entity Framework 6, WPF/MVVM,
ASP.NET (Framework), SQL Server (LocalDB for the sample).

**Architecture**

WPF View → ViewModel → WCF proxy → IInventoryService → service →
EF DbContext → SQL Server (LocalDB). The ASP.NET web app is a second client
of the same service. Bindings: BasicHttp for the web client, NetTcp for the
WPF client.

**Why this exists**

Built as portfolio evidence for WCF and Azure DevOps YAML pipeline
experience. Path B (modern .NET + CoreWCF + EF Core + ASP.NET Core) follows
later as an update to this same project.

**How to run**

(To be completed when the solution lands.)

**Pipelines**

- GitHub Actions: build on push/PR to main (Windows runner).
- Azure DevOps: azure-pipelines.yml — build, test, publish stages.
