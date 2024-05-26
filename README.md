

# C# to C++ Compiler/Transpiler

**Disclaimer: This project is currently in its infancy and is not functional. Expect significant changes and limitations.**

This project aims to develop a compiler/transpiler that converts C# code into C++, providing compatibility with C++ libraries and classes seamlessly. It facilitates the integration of C++ functionalities into C# projects with minimal effort.

## Features (Planned)

- **C# to C++ Compilation:** Convert C# code into equivalent C++ code.
- **Integration with C++ Libraries:** Easily incorporate C++ libraries and classes into C# projects.
- **Syntax for C++ Integration:** Intuitive syntax for utilizing C++ features within C# code.
- **Automatic Transpilation:** Streamline the process of converting C# code to C++ with automated tools.

## Example Usage (Not Yet Functional)

```csharp
using Lib["./class.h"]; // Import C++ library/header file

using System;

public static void Main()
{
    [CppLib("./version.h")]
    CheckForUpdateFromCpp("version"); // Call C++ function from version.h
    
    CppClass newInstance = new CppClass(); // Instantiate C++ class from class.h
    
    Console.WriteLine(newInstance.Name);
}
```

## Contribution

This project is open to contributions! If you're interested in contributing, please feel free to explore the codebase and open issues or pull requests.
