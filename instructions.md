# Copilot Instructions

These instructions apply to the entire `IPT_VelascoPersonalWebsite` repository.

## Project Context

- This is an ASP.NET Web Forms application targeting **.NET Framework 4.8.1**.
- The project uses a legacy, non-SDK-style `.csproj` file and `packages.config`.
- The default development host is IIS Express; HTTPS port `44377` is configured.
- Read `DOCUMENTATION.md` before making changes that affect application structure or shared behavior.

## Framework and Compatibility Rules

- Preserve .NET Framework 4.8.1 compatibility.
- Use Web Forms patterns already present in the repository: ASPX pages, code-behind files, designer files, master pages, ASCX controls, `Global.asax`, and `App_Start` configuration.
- Do not assume ASP.NET Core, .NET 5+, dependency injection hosting, Razor Pages, MVC Core, minimal APIs, or SDK-style project behavior.
- Do not migrate the project or upgrade packages unless the user explicitly requests modernization.
- Prefer existing dependencies and assets over adding new packages or frameworks.

## Repository Investigation

- Inspect the relevant project file and neighboring source before editing.
- Treat `IPT_VelascoPersonalWebsite.csproj` as authoritative for explicit content and compile items.
- Confirm that a referenced page or control actually exists before relying on its behavior. The project may retain template references such as `Default.aspx`, `About.aspx`, or `Contact.aspx`.
- For shared behavior, inspect `Global.asax.cs`, `App_Start/RouteConfig.cs`, `App_Start/BundleConfig.cs`, and `Site.Master` first.
- Do not infer database, API, authentication, or test infrastructure that is not present in the repository.

## Editing Rules

- Make the smallest change that satisfies the request.
- Preserve the existing namespace: `IPT_VelascoPersonalWebsite`.
- Preserve the existing indentation, naming, and code organization in each file.
- Avoid unrelated cleanup, broad formatting changes, or generated-file churn.
- Keep markup, code-behind, and designer files synchronized when changing Web Forms controls.
- Update the legacy project file when adding a file that requires explicit `Content`, `Compile`, or `None` entries.
- Put shared layout changes in `Site.Master` and shared styling in `Content/Site.css` unless the request requires a different scope.
- When modifying bundles, preserve JavaScript dependency order and verify the bundle paths used by the master page.
- Do not add comments unless they clarify a non-obvious behavior or match the surrounding file style.
- Do not commit secrets, credentials, local machine paths, build output, or package caches.

## Validation Rules

- Build the project or solution after code/configuration changes.
- For Web Forms changes, check both the associated code-behind compilation and the markup/control wiring.
- Run relevant tests if a test project is added or becomes available; currently no automated test project is documented.
- For UI changes, perform a browser smoke test with IIS Express when possible.
- Check that debug and release configuration behavior remains intentional, especially around `Web.config` transforms.
- Report pre-existing build or repository issues separately from issues introduced by the change.

## Documentation Rules

- Update `DOCUMENTATION.md` when architecture, routes, dependencies, startup behavior, setup, or known limitations change.
- Keep documentation factual and distinguish implemented behavior from planned or template content.
- Record new operational prerequisites and non-obvious deployment steps.
- Keep this file focused on instructions for coding agents; put general system documentation in `DOCUMENTATION.md`.

## Response Expectations

When completing a task:

1. Briefly summarize the files changed and the behavior affected.
2. State the validation performed, including build/test results.
3. Mention relevant limitations or follow-up work without inventing functionality.
