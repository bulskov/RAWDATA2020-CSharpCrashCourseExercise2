# RAWDATA2020-CSharpCrashCourseExercise2

This is the solution to the second exercise of the C# crash course.

I have added some comments in the code to explain the algorithms, and links to Wikipedia pages for most parts related to the problem solution. 

I also included unit testing to this problem. Primary because when working with problems like this, where you have input that is mapped to specific output, testing is very helpful. 
You can work your way through the solution by defining your expectations and the find solutions. This way of working leads to a methodology calls Test-Driven Development (TDD). 
We will talk much more about testing and briefly touch upon TDD in classes. 

As you can see from the structure of this solution, I have two projects, one with the program and one with the related tests. This is not the only way to organize your code, but a very common way to 
separate production and development code.

I use the Xunit testing framework together with and fluent assertions, both packages to be found on NuGet. NuGet is the package manager used when working with .NET. We will also talk and use NuGet later in the course.
If you clone this project the NuGet packages will be downloaded by Visual Studio automatically. If you are on Visual Studio Code, just use the "dotnet restore" command from the command line.
