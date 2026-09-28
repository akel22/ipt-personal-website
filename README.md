# Ezekiel John Velasco — Personal Portfolio

A static, single-page personal portfolio site. No backend, database, or build step.

## Live Sections
- **About** — introduction and quick contact links
- **Education** — academic timeline
- **Interests** — hobbies and interests
- **Skills** — technologies and tools
- **Contact** — email CTA and footer contact details

## Tech Stack
- HTML5
- Bootstrap 5 (CSS + bundled JS)
- Vanilla JavaScript (minimal, no frameworks)

## Project Structure
```
index.html          Single-page site markup
css/
  site.css          Custom styles
  vendor/           Vendored Bootstrap CSS
js/
  main.js           Minimal JS (footer year)
  vendor/           Vendored Bootstrap bundle JS (navbar collapse only)
assets/svg/          Skill icons and brand/social icons
vercel.json          Vercel deployment config
```

## Running Locally
Open `index.html` directly in a browser, or serve the folder with any static file server:
```bash
npx serve .
```

## Deploying to Vercel
No build command is required.
- Framework preset: **Other**
- Build command: none
- Output directory: repository root

## Content Notes
- Section text is hardcoded directly in `index.html`.
- The Skills section content and icons are intentionally fixed — see [`.github/copilot-instructions.md`](.github/copilot-instructions.md).

See [DOCUMENTATION.md](DOCUMENTATION.md) for additional technical notes.
