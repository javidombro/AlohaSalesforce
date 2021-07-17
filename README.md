# Aloha Salesforce

## _Salesforce interview excercise_

Components of computer systems often have dependencies (i.e., other components that must be installed before the systems function properly). These dependencies are frequently shared by multiple components. For example, both the TELNET client program and the FTP client program require that the TCP/IP networking software be installed before they can operate. If you install TCP/IP and the TELNET client program, and later decide to add the FTP client program, you do not need to reinstall TCP/IP.
For some components, it would not be a problem if the components on which they depended were reinstalled; it would just waste some resources. For other components (e.g., TCP/IP), some component configuration may be destroyed if the component were reinstalled.
It is useful to be able to remove components that are no longer needed. When this is done, components that only support the removed component may also be removed, which frees up disk space, memory, and other resources. A supporting component that is not explicitly installed may be removed only if all components that depend on it are also removed. For example, removing the FTP client program and TCP/IP would mean the TELNET client program, which was not removed, would no longer operate. Likewise, removing TCP/IP by itself would cause the failure of both the TELNET and the FTP client programs. Also, if we installed TCP/IP to support our own development, then installed the TELNET client (which depends on TCP/IP), and then still later removed the TELNET client, we would not want TCP/IP to be removed. 
Dependence is transitive. For example, if A depends on B, and B depends on C, both B and C are implicitly installed when A is explicitly installed. Conversely, B and C would both be removed if A is subsequently removed. We need a program to automate the process of adding and removing components. To do this, we will maintain a record of installed components and component dependencies. A component can be installed explicitly in response to a command (unless it has already been explicitly installed), or implicitly if it is needed by some other component being installed. A component can be explicitly removed in response to a command (if it is not needed to support other components) or implicitly removed if it is no longer needed to support another component (and has not been explicitly installed).


## Commands

- DEPEND item1 item2 [item3...]: item1 depends on item2 (and item3 ...)
- INSTALL item1: install item1 and those on which it depends
- REMOVE item1: remove item1, and those on which it depends, if possible
- LIST: list the names of all currently-installed components

## Design Patterns used in this project

- SINGLETON: I used singleton to have only one instance of each component.
- COMMAND: I used the command pattern to be able to add new functionalities in future without making much changes.

## Instruction to run the code

To run the application you need to have installed the [.NET SDK](https://docs.microsoft.com/en-us/dotnet/core/install/windows).

Once installed go to the solution folder and run the following commands:

```sh
dotnet build
dotnet run --project .\AlohaSalesforce\AlohaSalesforce.csproj
```
Once the program is running, it will be reading the commands from the stdin.
To finish the excecution type:
```sh
END
```

To run tests:

```sh
dotnet build
dotnet test
```


## License

MIT
