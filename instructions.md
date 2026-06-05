# Roadmap: Building a .NET Markdown Editor for Windows

This document serves as your complete engineering roadmap and comprehensive prompt reference. You can feed this entire file directly into your local Large Large Model (LLM) or Antigravity Agent to guide you step-by-step through setting up, developing, testing, and packaging your application.

---

## 1. Project Overview & Architectural Blueprint

### Target Outcome
A lightweight, lightning-fast, and distraction-free Windows desktop Markdown editor built entirely within the modern **.NET 8/9** ecosystem.

### Selected Technical Stack
* **Shell/Host:** .NET MAUI (Multi-platform App UI) configured specifically for Windows Desktop (WinUI3 backend).
* **UI Layer:** Blazor Hybrid. This embeds a native Chromium-based `BlazorWebView` component inside the desktop application shell.
* **Logic/Processing:** 100% C# for the component interactions, UI state, application lifecycle, and file system management.
* **Markdown Parsing:** `Markdig` (an ultra-fast, extensible, and compliant .NET markdown processor).
* **Styling/Themes:** Clean CSS with custom variables supporting instant Light/Dark mode transitions.

### Why This Architecture?
* **Native OS Integration:** Unlike Electron, which bundles a heavy standalone browser instance, .NET MAUI leverages the native Windows Edge/Chromium WebView2 runtime already baked into Windows 10 and 11. This drastically cuts down on idle memory overhead.
* **Direct File System Access:** Zero sandboxing barriers. The application communicates natively with the local Windows kernel using standard `.NET APIs` (`System.IO`).
* **Seamless HTML Translation:** Because Markdown parses natively into HTML, rendering the live preview within a Blazor Hybrid layout eliminates the complex XAML parsing overhead required by traditional native UI layers.

### Component Layout Design
```
+-------------------------------------------------------------------------+
|  [File: New | Open | Save]                     [Theme: Light/Dark]      |
+-------------------------------------------------------------------------+
|                                    |                                    |
|  EDITOR PANE                       |  LIVE PREVIEW PANE                 |
|  <textarea> or Code Editor          |  Rendered HTML via Markdig         |
|                                    |                                    |
|  # Document Title                  |  Document Title (H1)               |
|  This is markdown text...          |  This is markdown text...          |
|                                    |                                    |
|                                    |                                    |
|                                    |                                    |
+-------------------------------------------------------------------------+
| Lines: 12 | Words: 45                                      Saved / Dirty|
+-------------------------------------------------------------------------+
```

---

## 2. Step-by-Step Implementation Phase Checklist

### Phase 1: Environment & Project Initialization
* [ ] Verify the system has **Visual Studio 2022** (v17.8+) installed with the **.NET Multi-platform App UI development** workload actively selected.
* [ ] Initialize a new project from the Command Line Interface (CLI) or Visual Studio template wizard:
  ```bash
  dotnet new maui-blazor -o MarkdownEditorApp
  ```
* [ ] Clean out boilerplate template components (such as `Counter.razor`, `FetchData.razor`, and default styling assets) to establish a clean workbench.
* [ ] Install required NuGet packages using the Package Manager Console or CLI:
  ```bash
  dotnet add package Markdig
  ```

### Phase 2: Configuration & The Core Window
* [ ] Modify `MauiProgram.cs` to configure the desktop window launch dimensions (e.g., standard desktop size: `1200x800`), disable unnecessary cross-platform hooks, and ensure platform services initialize seamlessly.
* [ ] Locate and verify target layout structure in `Platforms/Windows/App.xaml.cs` or `MauiProgram.cs` to guarantee window behavior matches clean desktop conventions (centering on screen, minimal canvas flickers).

### Phase 3: Layout & Core Core Service Engine
* [ ] Define a clear state structure or create a dedicated service class (`EditorState.cs`) to track:
  * Current file workspace path (`string? CurrentFilePath`).
  * Raw markdown string working copy (`string MarkdownText`).
  * Document state toggle (`bool IsDirty`).
* [ ] Construct the primary UI interface layer in `MainLayout.razor` or a main workspace view using a split-pane structural grid block (Editor view occupying 50% horizontal width, Preview view occupying the remaining 50%).

### Phase 4: Core Markdown Features Integration
* [ ] Create a standard high-performance `<textarea>` component within the left pane, binding it to the local string variable using `@bind:event="oninput"` to capture every single keystroke.
* [ ] Wire up the `Markdig` pipeline engine within a computing method or property to dynamically compile the string input text:
  ```csharp
  // Basic translation pipeline execution
  var htmlContent = Markdig.Markdown.ToHtml(MarkdownText);
  ```
* [ ] Output the processed string into the right-hand preview panel container using Blazor’s explicit `(MarkupString)` cast mechanism to instruct the layout engine to render valid HTML tags directly instead of escaping them as plain text.

### Phase 5: Local OS File System Access Mechanics
* [ ] Integrate native operating system save and open dialog boxes using .NET MAUI standard platform interop utilities (`Microsoft.Maui.Storage.FilePicker`).
* [ ] Implement secure standard asynchronous local file reading loops:
  ```csharp
  string content = await File.ReadAllTextAsync(pickedFile.FullPath);
  ```
* [ ] Implement standard local streaming file writing hooks (`File.WriteAllTextAsync`) matching current workspace context coordinates.

### Phase 6: Polish, Styles, & Production Packaging
* [ ] Write robust cascading CSS variables to govern Light/Dark interfaces, mapping responsive text alignments, code-block fonts, and margins across both panes.
* [ ] Set up keyboard event triggers (such as trapping `Ctrl + S` actions inside input blocks) to execute background save workflows.
* [ ] Execute the final compilation profile and generate a production-ready package optimized for Windows architectures using the standard native installer publish profile:
  ```bash
  dotnet publish -f net8.0-windows -c Release
  ```

---

## 3. Modular LLM / Agent Prompt Sequence

Use the following highly descriptive instructions sequentially to build the application piece-by-piece.

### Prompt 1: Project Scaffolding & Initial Window Tweaks
```text
Act as an expert .NET desktop engineer. I am creating a Markdown Editor app for Windows using .NET MAUI Blazor Hybrid. 
I have created a fresh project using the command `dotnet new maui-blazor`. 

Please write the complete code for `MauiProgram.cs` that configures the application framework properly. 
Inside this configuration, include platform-specific window lifecycle overrides for Windows to ensure that when the app boots, the initial window width is explicitly set to 1200 pixels, the height is set to 800 pixels, and it launches centered on the user's screen. 
Remove or ignore code patterns designed for iOS or Android to keep the setup as clean and lean as possible for a native Windows application.
```

### Prompt 2: Core Editor Components & Markdown Parsing Layout
```text
Act as a front-end Blazor developer. I need the primary workspace UI for my Markdown Editor. 
I have added the `Markdig` NuGet package to my project. 

Please create a self-contained Blazor component named `MarkdownWorkspace.razor`. 
The UI must use a split-pane layout (left column for writing, right column for previewing) using clean CSS display blocks suitable for BlazorWebView.

Requirements:
1. The left side must have a borderless, full-height `<textarea>` that binds instantly to a `MarkdownText` string variable on every single keystroke (use the oninput event).
2. The right side must act as the live preview container. Take the `MarkdownText`, parse it into HTML using `Markdig.Markdown.ToHtml()`, and output it safely onto the page using `(MarkupString)`.
3. Include inline CSS styles directly in a `<style>` block inside this component to give the editor a modern dark mode design aesthetic: a dark slate background (#1e1e1e), crisp light text (#e3e3e3), a distinct subtle vertical divider between the two columns, and clean margins for rendered typography (headers, code blocks, lists).
```

### Prompt 3: Native File System Integration Service
```text
Act as a backend .NET developer specializing in Windows file I/O operations. I need a robust architecture to open, track, and save plain text markdown files (`.md`) directly to the local Windows hard drive.

Please write a C# service or component backend code-behind that integrates with my workspace component. 
Implement three core asynchronous methods using standard native platform tools:
1. `OpenFileAsync()`: Uses .NET MAUI's `FilePicker` filtered exclusively to `.md` and `.txt` extensions. It must read the text contents from disk, populate our editor's text string, and store the absolute file path.
2. `SaveFileAsync()`: Writes the current raw editor string back to the currently loaded file path. If no path is actively tracked yet (a new unsaved document), it must automatically forward execution to the `SaveFileAsAsync()` method.
3. `SaveFileAsAsync()`: Prompts the user with a file target dialog to specify a location and file name on their system, saves the contents, and updates the active file path context tracker.

Ensure excellent defensive error handling is included via try-catch blocks to catch OS-level permission issues or unexpected cancellations, notifying the app state gracefully.
```

### Prompt 4: UI Enhancements, Shortcuts, & Workspace Metrics
```text
Let's add the final professional features to our Blazor Markdown editor layout. I want to build a clean status bar at the very bottom of the application window and add system shortcut integrations.

Please provide updated code and logic enhancements to handle:
1. A Bottom Status Bar: Create a horizontal strip at the bottom of the window that displays:
   - Total current Word Count (calculated dynamically via a C# property parsing the text string).
   - Total current Line Count (splitting text by newline characters).
   - A clean "Saved" vs "Unsaved Changes" text indicator that flips to "Unsaved Changes" whenever the textarea experiences an input modification event, and resets to "Saved" immediately following a successful disk write execution.
2. Keyboard Intercepts: Add a clean Blazor layout event listener to intercept key combinations within the editor container so that pressing `Ctrl + S` triggers our file save routine automatically without forcing the user to click a menu button.
```

---

## 4. Local Testing & Verification Matrix

| Verification Target | Test Strategy | Expected Outcome | Pass/Fail |
| :--- | :--- | :--- | :--- |
| **Real-time Synchronization** | Type `# Hello World` inside the input textarea. | The preview pane immediately updates to show a large, bold H1 element without visual layout lag. | |
| **Syntax Extensibility** | Input a standard markdown list (`- Item 1
- Item 2`). | The preview pane outputs structured HTML list items (`<ul>`, `<li>`) cleanly spaced. | |
| **OS File Retrieval** | Click 'Open File', choose a pre-existing `.md` document. | The textarea completely populates with the text, the preview renders perfectly, and the internal file tracker updates. | |
| **Silent Save Execution** | Edit text on an opened file and press the `Ctrl + S` key combination. | The status bar immediately transitions from "Unsaved Changes" to "Saved", and changes appear on disk without popping a new dialog box. | |
| **Clean Exit Operations** | Close the app while the status bar reads "Unsaved Changes". | Future enhancements: Implement a warning confirmation box (can be added by extending native Maui window closure handlers). | |