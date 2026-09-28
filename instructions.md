# Copilot Instructions

These instructions apply to the entire repository.

## Project Context

- This is a static HTML/CSS/Bootstrap/JS personal portfolio site with no backend, build step, or database.
- Entry point is `index.html` at the repo root. It is a single scrolling page with anchor navigation (`#about`, `#education`, `#interests`, `#skills`, `#contact`).
- Styling: Bootstrap CSS (`css/vendor/bootstrap.min.css`) plus custom rules in `css/site.css`.
- Scripting: Bootstrap's bundled JS (`js/vendor/bootstrap.bundle.min.js`, used only for the responsive navbar) and a minimal `js/main.js` (currently just sets the footer year).
- Assets live under `assets/svg/` (skill icons and brand/social icons). There is no `assets/img/` folder.
- Deployment target is Vercel as a static site: no build command, output directory is the repo root.

## Editing Rules

- Make the smallest change that satisfies the request.
- Keep the stack simple: plain HTML/CSS/Bootstrap/vanilla JS. Do not introduce a framework, bundler, package.json, or build step unless explicitly requested.
- Avoid heavy animations or added JS complexity; this site is intentionally minimal.
- Do not add a contact form or any server-dependent feature — contact is via `mailto:` and social links only.
- Do not commit secrets, credentials, local machine paths, or editor/IDE metadata.

## Skills Section

- Keep the existing skill names, order, and SVG icons in `#skills` exactly as they are. Do not make this section editable/customizable or data-driven.


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
