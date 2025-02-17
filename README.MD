# Request Trail

![Project Banner](/media/project-banner.png)

This project consists in logging all the trail between different APIs, saving the CorrelationId among the requests. The code that handles this is in a NuGet package that I've created

I created 2 APIs:
- CallerAPI
    - The API that does the main request, and calls a service from CalledApi
- CalledApi
    - The API that it's called, sharing the CorrelationId with CallerApi, which is where it was initially generated
- Trailing
    - It's a NuGet package where it has all the shared code that handles the trailing logs

I have here a diagram that shows better how this project works:

![Project Diagram](/media/project-diagram.png)

And a image from the logs where you can see what I've explained:

![Presentation](/media/presentation.png)


