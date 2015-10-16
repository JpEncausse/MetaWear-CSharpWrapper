# MetaWear C# Wrapper
The project constructs a C# wrapper around the MetaWear C++ API.

# Download
The source code can be downloaded with a direct link or cloned.  If downloading with a link, you will also need to download the 
corresponding commit of the Metawear-CppAPI project and put the files in the metwear-cpp-api folder.  If cloning with git, you 
can run 'git submodule update --init' to pull in the C++ api.

```sh
> git clone http://github.com:mbientlab/MetaWear-CSharpWrapper.git
> cd MetaWear-CSharpWrapper/
> git submodule update --init
```

# Build
The project is built as a Windows Phone 8.1 class library using Visual Studio Community 2015.  You can simply open the solution 
file and build the WrapperTest app to build both the C++ api and C# wrapper classes.  The test app can be loaded onto your 
phone to see the library in action.
