# FollowTheTask
A simple task tracking system implemented using **ASP.NET MVC 5**

Takes quite a long time for the first launch and first requests to database. This is due to **AppHarbor** hosting policy for free plans. After visiting several pages it gets quite smooth

## Features
1. Uses **ASP.NET Identity** user registration
2. Uses e-mail confirmation for some actions(through **SMTP** protocol)
3. Administrator panel for managing roles and users.
4. Has a **SignalR** chat for communication between singed in users
5. Has a **WCF** service for retrieving readonly information from website. Available at http://website_address/Service/Service.svc 

## Description
Allows to register new user as manager, worker or both.
Manager can hire workers or accept their requests, create tasks and quests, assign workers for quests.
Worker can apply to a manager or accept a request from manager, accomplish quests.