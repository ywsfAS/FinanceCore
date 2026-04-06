# CoinHive

CoinHive is a modular React + TypeScript frontend project for FinanceCore, designed to provide interactive dashboards, financial analytics, and user interfaces for finance management. The project leverages component-based architecture, modern styling, and React Router for seamless navigation.

---

## 🗂 Project Structure

```text
coinhive/
├─ .vscode/                  # VSCode workspace settings
├─ node_modules/             # NPM dependencies
├─ public/                   # Static assets
├─ src/
│  ├─ assets/                # Images, logos, icons
│  ├─ components/            # Reusable React components
│  │  ├─ AboutHero/
│  │  ├─ AboutSection/
│  │  ├─ BarChartCard/
│  │  ├─ BarTooltip/
│  │  ├─ Button/
│  │  ├─ Card/
│  │  ├─ Checkbox/
│  │  ├─ ContactForm/
│  │  ├─ ContactHero/
│  │  ├─ ContactInfo/
│  │  ├─ Ctasection/
│  │  ├─ Faqsection/
│  │  ├─ FeaturesSection/
│  │  ├─ FinancialOverview/
│  │  ├─ FinancialTransaction/
│  │  ├─ Footer/
│  │  ├─ HeroBalance/
│  │  ├─ HeroSection/
│  │  ├─ Howitworks/
│  │  ├─ Input/
│  │  ├─ LogoSection/
│  │  ├─ Navbar/
│  │  ├─ PieChartCard/
│  │  ├─ PieTooltip/
│  │  ├─ Pricingsection/
│  │  ├─ SideImage/
│  │  ├─ SpendingAnalytics/
│  │  ├─ StatCard/
│  │  ├─ StatsGrid/
│  │  ├─ TagButtons/
│  │  ├─ TeamSection/
│  │  ├─ TestimonialsSection/
│  │  └─ ValueSection/
│  ├─ context/               # React Context providers
│  ├─ entities/              # Domain entities or models
│  ├─ hooks/                 # Custom React hooks
│  ├─ pages/                 # Page-level components
│  ├─ routes/                # App routing configuration
│  ├─ services/              # API or utility services
│  ├─ styles/                # Global and shared styles
│  ├─ use-cases/             # Business logic / use-case implementations
│  ├─ App.tsx
│  ├─ App.css
│  ├─ index.css
│  └─ main.tsx
├─ coinhive.esproj            # Project configuration
├─ package.json
├─ package-lock.json
├─ tsconfig.app.json
├─ tsconfig.json
├─ tsconfig.node.json
├─ vite.config.ts
├─ README.md
└─ CHANGELOG.md

```
# CoinHive Project Overview


##  Technologies Used
- **React 18** – Component-based frontend  
- **TypeScript** – Type-safe JavaScript  
- **Vite** – Fast development server and build tool  
- **React Router** – Page navigation  
- **Module CSS Variables** – For local scope scalable styling  
- **Custom Components** – Buttons, cards, charts, inputs, and more  

---

##  Features
- **Landing Page:** Hero section, features, logos, testimonials, pricing, CTA  
- **About Page:** Team, mission, values, story sections  
- **Authentication:** Register and login pages  
- **Dynamic Components:** Reusable UI components like Cards, Charts, Hero sections  
- **Responsive Design:** Mobile-first layouts  
- **Finance Dashboard Components:** SpendingAnalytics, FinancialOverview, FinancialTransaction  
- **Charts:** PieChartCard, BarChartCard, and custom tooltips  
- **Forms:** Input fields, checkboxes, buttons with validation  
- **Routing:** Pages linked using React Router (`Routes`, `Route`, `NavLink`)  

---

## 🏗 Project Setup

1. **Install Dependencies**  
```bash
npm install
npm run dev
```
## 📁 Folder Conventions
- **components/** – Reusable UI elements
- **pages/** – Page-level components (Landing, About, Login, Register)
- **hooks/** – Custom hooks for state and side effects  
- **context/** – React Context providers
- **services/** – API or business logic
- **use-cases/** – Domain-specific use cases
- **entities/** – Data models
- **styles/** – Global and shared CSS