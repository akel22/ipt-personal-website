<%@ Page Title="Portfolio" Language="C#" MasterPageFile="~/Authenticated.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="VelascoPersonalWebsite_IPT.Home" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <meta name="description" content="Personal portfolio of a student and aspiring information technology professional." />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <section id="about" class="container py-5 py-lg-6">
            <div class="row align-items-center g-5">
                <div class="col-lg-7">
                    <h1 class="display-4 fw-bold mb-3">Hello, I'm <span class="text-secondary">EJ</span>.</h1>
                    <p class="lead text-secondary mb-4">An Information Technology student aspiring to be a Backend/ Data Engineer.</p>
                    <a class="btn btn-dark px-4 py-2 me-2" href="#contact">Get in touch</a>
                    <a class="btn btn-outline-dark px-4 py-2" href="#education">Explore my journey</a>
                </div>
                <div class="col-lg-5">
                    <div class="bg-white border rounded-4 p-4 p-md-5">
                        <h2 class="h5 fw-semibold mb-4">Let's connect</h2>
                        <div class="d-flex flex-column gap-3">
                            <a class="d-flex align-items-center gap-3 text-decoration-none text-dark" href="mailto:velasco.ezekieljohn.javellana@gmail.com">
                                <img class="portfolio-social-icon" src="<%= ResolveUrl("~/assets/svg/gmail_logo.svg") %>" alt="" width="28" height="28" />
                                <span>velasco.ezekieljohn.javellana@gmail.com</span>
                            </a>
                            <a class="d-flex align-items-center gap-3 text-decoration-none text-dark" href="https://www.linkedin.com/in/ezekiel-john-velasco-2a15a2417/" target="_blank" rel="noopener noreferrer">
                                <img class="portfolio-social-icon" src="<%= ResolveUrl("~/assets/svg/linkedin_logo.svg") %>" alt="" width="28" height="28" />
                                <span>linkedin.com/in/ezekiel-john-velasco</span>
                            </a>
                            <a class="d-flex align-items-center gap-3 text-decoration-none text-dark" href="https://github.com/akel22" target="_blank" rel="noopener noreferrer">
                                <img class="portfolio-social-icon" src="<%= ResolveUrl("~/assets/svg/github_logo.svg") %>" alt="" width="28" height="28" />
                                <span>github.com/akel22</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section id="education" class="bg-white border-top border-bottom">
            <div class="container py-5 py-lg-6">
                <div class="row g-4 g-lg-5">
                    <div class="col-lg-4">
                        <h2 class="h2 fw-bold mb-3">Educational attainment</h2>
                        <p class="text-secondary mb-0">The milestones that continue to shape my path in technology.</p>
                    </div>
                    <div class="col-lg-8">
                        <div class="border-start border-3 ps-4 mb-4">
                            <p class="small fw-semibold text-secondary mb-1">2024 - Present</p>
                            <h3 class="h5 fw-semibold mb-1">Information Technology</h3>
                            <p class="text-secondary mb-0">Currently pursuing an Information Technology degree.</p>
                        </div>
                        <div class="border-start border-3 ps-4">
                            <p class="small fw-semibold text-secondary mb-1">2022 - 2024</p>
                            <h3 class="h5 fw-semibold mb-1">Senior High School Graduate</h3>
                            <p class="text-secondary mb-0">Completed a Science, Technology, Engineering, and Mathematics (STEM) strand.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section id="interests" class="container py-5 py-lg-6">
            <div class="row g-4">
                <div class="col-md-5">
                    <h2 class="h2 fw-bold mb-3">Hobbies and interests</h2>
                    <p class="text-secondary mb-0">A few simple things that keep me curious, focused, and inspired.</p>
                </div>
                <div class="col-md-7">
                    <div class="row g-3">
                        <div class="col-sm-4"><div class="bg-white border rounded-3 p-4 h-100"><h3 class="h5 fw-semibold mb-2">Studying</h3><p class="small text-secondary mb-0">Learning something new every day.</p></div></div>
                        <div class="col-sm-4"><div class="bg-white border rounded-3 p-4 h-100"><h3 class="h5 fw-semibold mb-2">Music</h3><p class="small text-secondary mb-0">Listening to music and discovering new sounds.</p></div></div>
                        <div class="col-sm-4"><div class="bg-white border rounded-3 p-4 h-100"><h3 class="h5 fw-semibold mb-2">Movies</h3><p class="small text-secondary mb-0">Watching movies and exploring different stories.</p></div></div>
                    </div>
                </div>
            </div>
        </section>

        <section id="skills" class="bg-white border-top border-bottom">
            <div class="container py-5 py-lg-6 text-center">
                <p class="portfolio-eyebrow text-uppercase mb-2">Skills &amp; experience</p>
                <h2 class="h2 fw-bold mb-3">Working with technologies and tools</h2>
                <p class="text-secondary mx-auto mb-5 portfolio-skills-copy">A growing toolkit for building reliable applications, data solutions, and thoughtful digital experiences.</p>

                <div class="portfolio-skills-grid mx-auto">
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/python.svg") %>" alt="Python" /><span>Python</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/csharp.svg") %>" alt="C sharp" /><span>C#</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/java.svg") %>" alt="Java" /><span>Java</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/javascript.svg") %>" alt="JavaScript" /><span>JavaScript</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/typescript.svg") %>" alt="TypeScript" /><span>TypeScript</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/html-5.svg") %>" alt="HTML 5" /><span>HTML</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/css.svg") %>" alt="CSS" /><span>CSS</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/asp-net-core.svg") %>" alt="ASP.NET Core" /><span>ASP.NET</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/django.svg") %>" alt="Django" /><span>Django</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/postgresql-dark.svg") %>" alt="PostgreSQL" /><span>PostgreSQL</span></div>
                    <div class="portfolio-skill-item"><img src="<%= ResolveUrl("~/assets/svg/mongodb.svg") %>" alt="MongoDB" /><span>MongoDB</span></div>
                </div>
            </div>
        </section>

        <section id="contact" class="container py-5 py-lg-6">
            <div class="bg-dark text-white rounded-4 p-4 p-md-5">
                <div class="row align-items-start g-4 g-lg-5">
                    <div class="col-lg-5">
                        <h2 class="display-6 fw-bold mb-3">Get in touch</h2>
                        <p class="text-white-50 mb-0 portfolio-contact-copy">Have a question, opportunity, or idea to share? Send me a message and I will get back to you.</p>
                    </div>
                    <div class="col-lg-7">
                        <div class="portfolio-contact-form">
                            <div class="mb-3">
                                <label class="form-label" for="contactName">Name</label>
                                <input id="contactName" name="contactName" type="text" class="form-control" placeholder="Your name" required />
                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="contactEmail">Email address</label>
                                <input id="contactEmail" name="contactEmail" type="email" class="form-control" placeholder="you@example.com" required />
                            </div>
                            <div class="mb-3">
                                <label class="form-label" for="contactDescription">Description of email</label>
                                <textarea id="contactDescription" name="contactDescription" class="form-control" rows="4" placeholder="How can I help you?" required></textarea>
                            </div>
                            <button type="submit" class="btn btn-light px-4 py-2">Send message</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </main>

    <footer class="portfolio-footer bg-black text-white">

        <div class="container py-5 py-lg-6">
            <div class="row align-items-start justify-content-between g-5">
                <div class="col-12 col-lg-5">
                    <p class="portfolio-footer-statement text-uppercase fw-bold mb-4">Just keep<br />learning.</p>
                    <p class="small text-white-50 mb-0">&#169;<%: DateTime.Now.Year %> Ezekiel John Velasco.<br />Digital Portfolio</p>
                </div>

                <div class="col-12 col-sm-6 col-lg-4 col-xl-3">
                    <h2 class="portfolio-footer-heading text-uppercase mb-3">Contact</h2>
                    <ul class="list-unstyled portfolio-footer-list mb-0">
                        <li>velasco.ezekieljohn.javellana@gmail.com</li>
                        <li>+63 928-494-8326</li>
                        <li>GitHub: akel22</li>
                        <li>LinkedIn: Ezekiel John Velasco</li>
                    </ul>
                </div>
            </div>

        </div>
    </footer>
</asp:Content>
