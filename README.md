# CSharp-Toolbox

![Language](https://img.shields.io/badge/language-C%23-178600)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-active-success)
![Build](https://img.shields.io/badge/build-passing-brightgreen)

**CSharp-Toolbox is a shared library repository containing reusable C# components used across multiple Linktech Engineering projects.**  
It is **not** a standalone application. Instead, it provides foundational utilities, UI components, and shared logic consumed by other solutions such as Medical and future Visual Studio 2026–aligned projects.

This repository centralizes common functionality into a single, well‑structured, deterministic codebase to ensure consistency, maintainability, and architectural clarity across the entire ecosystem.

---

## 📁 Repository Structure

CSharp-Toolbox/
│
├── Tools/
│   ├── BaseTools/     # Core reusable logic and foundational utilities
│   ├── DbTools/       # Database helpers, schema routines, migrations
│   ├── ToolsUI/       # Shared UI components and reusable WinForms controls
│   └── README.md
│
├── .gitignore
├── LICENSE
└── README.md


Each folder contains modular, self‑contained components designed to be imported into other C# solutions.

---

## 🎯 Purpose

The goal of this repository is to:

- Provide a **centralized library** of reusable C# components  
- Ensure **deterministic behavior** across all consuming projects  
- Maintain **consistent architecture** and **shared patterns**  
- Reduce duplication across Medical, future tools, and VS2026 migrations  
- Serve as the **foundation layer** for all C# development within Linktech Engineering  

---

## 🧱 Design Philosophy

CSharp‑Toolbox follows the same engineering principles used across the entire suite:

- **Deterministic execution**  
- **Operator‑grade reliability**  
- **Clear separation of concerns**  
- **No registry dependencies**  
- **Unified configuration patterns**  
- **Audit‑transparent behavior**  
- **Modular, reusable components**  

This repo is intentionally structured to support long‑term maintainability and cross‑project integration.

---

## 🔗 Related Projects

- **Medical** — consumes Toolbox components (private)  
- **Visual Studio 2026 Migration** — ensures Toolbox compatibility  
- **NMS_Tools** — Python suite following similar deterministic patterns  
- **rust_logger** — Rust‑based logging utility  
- **PowerShell Modules** — deterministic operator tools  

---

## 📜 License

This project is licensed under the **MIT License**.

---
