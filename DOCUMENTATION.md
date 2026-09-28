# Personal Portfolio Site

A static, single-page personal portfolio. No backend, database, or build step.

## Stack
- HTML5
- Bootstrap 5 (CSS + bundled JS, vendored under `css/vendor` and `js/vendor`)
- A small amount of custom CSS (`css/site.css`) and vanilla JS (`js/main.js`)

## Structure
```
index.html          Single-page site (About, Education, Interests, Skills, Contact)
css/
  site.css          Custom styles (hero, skills grid, footer, etc.)
  vendor/           Vendored Bootstrap CSS
js/
  main.js           Minimal JS (sets footer copyright year)
  vendor/           Vendored Bootstrap bundle JS (navbar collapse only)
assets/svg/          Skill icons and brand/social icons
```

## Running locally
Open `index.html` directly in a browser, or serve the folder with any static file server, e.g.:
```
npx serve .
```

## Deploying to Vercel
No build command is required. In the Vercel project settings, use the "Other" framework preset with the output directory set to the repository root.

## Content changes
- Section text (About, Education, Interests, Contact, Footer) is hardcoded directly in `index.html`.
- The Skills section content and icons are intentionally fixed — see `.github/copilot-instructions.md`.
