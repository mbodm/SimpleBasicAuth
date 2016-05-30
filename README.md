# SimpleBasicAuth
A very simple basic-auth middleware PCL for ASP.NET Core

First: Do not use this code. It is a very bad approach to include basic authentication in an ASP.NET Core project. This PCL provides a generic middleware (and not a real authentication middleware) to handle basic authentication. If you want a more professional approach, have a look at the Odachi library by Kukkimonsuta (Lukáš Novotný).

Since i had a very limited timeframe, to get something really quick and dirty, and since ASP.NET Core improved the whole authentication and authorization systems, this silly approach was my alternative to get something really plain and simple, and instant. A clean, correct and fully testable real authentication middleware just needs more time.

Normally, as a professional software developer, i do not do stuff like that. I really hate it. Instead i usually invest the time and do it right. And in the rare cases i am forced to, i do not post it on GitHub :) But i needed a quick code sharing platform, and GitHub was just one click away. But aside from all of that, there is a reason why i do not care about in this particular case (and you should read that carefully, because it is important):

Basic authentication in general is totally insecure. You should never use it. Never. This is the reason why the .NET Core team do not include basic authentication in their authentication middlewares, and stay far away from it. Blowdart (Barry Dorrans) and other ASP.NET Core developers often mentioned it: Do not use it. Never never ever. No excuses. There are a lot of alternatives out there, and most of them are already shipped with ASP.NET Core (like i.e. cookie authentication). Use them instead.

So, there is no reason to not write this basic authentication stuff that simple and stupid, since you should not use it anyway. You can use that crap maybe for testing purposes, or demonstration purposes. Maybe. Or for your very own, very small, very insecure private projects (and not even there you should use it). But, since it is basic authentication, and therefore heavily insecure, you should never never ever use it for anything else. From this point of view, i do not really care about the horrible approach i used.

So keep these things in mind. And just do not use software like this in your projects.

Have a nice day.
