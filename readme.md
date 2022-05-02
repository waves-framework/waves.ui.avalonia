## <img style="float: right;" src="files/images/logo_64.png"/>  Waves Core
![logo](https://img.shields.io/github/license/waves-framework/waves.core) ![logo](https://img.shields.io/nuget/v/Waves.Core)

### 📚 About Waves

**Waves** is a cross-platform framework designed for flexible developing of desktop, mobile applications and web-services.

### 📒 About Waves.Core

**Waves.Core** is base kernel of Waves framework. It contains interfaces, base primitives, abstractions, services and utilities of framework. 

### 🚀 Getting started

Like all Waves libraries Waves.Core distributes via **NuGet**. You can find the packages [here](https://www.nuget.org/profiles/Waves).

Or use these commands in the Package Manager to install Waves.Core manually:

```
Install-Package Waves.Core
```

### ⌨️ Usage basics

After installing the package you just need to initialize core in your main class:

```c#
var core = new WavesCore();
await core.StartAsync();
await core.BuildContainerAsync();
```

Resolve services from container:

```c#
var logger = await core.GetInstanceAsync<ILogger<Program>>();
logger?.LogInformation("Hello world");
```

**⚠️ _Other documentation will be available soon._**

### 📋 Licence

Waves.Core is licenced under the [MIT licence](https://github.com/ambertape/waves.core/blob/master/license.md).
