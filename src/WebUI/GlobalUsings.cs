﻿global using AlgoCode.Application.Common.Attributes;
global using AlgoCode.Application.Common.Interfaces;
global using AlgoCode.Application.DTOs.Contests;
global using AlgoCode.Application.DTOs.Dashboard;
global using AlgoCode.Application.DTOs.Problems;
global using AlgoCode.Application.DTOs.Solutions;
global using AlgoCode.Application.Features.AppUser.Commands.LoginAdmin;
global using AlgoCode.Application.Features.AppUser.Commands.LoginUser;
global using AlgoCode.Application.Features.AppUser.Commands.RegisterUser;
global using AlgoCode.Application.Features.Comments.Commands.PostComment;
global using AlgoCode.Application.Features.Comments.Queries.GetAll;
global using AlgoCode.Application.Features.Contests.Commands.CreateContest;
global using AlgoCode.Application.Features.Contests.Commands.DeleteContest;
global using AlgoCode.Application.Features.Contests.Commands.UpdateContest;
global using AlgoCode.Application.Features.Contests.Queries.GetAll;
global using AlgoCode.Application.Features.Contests.Queries.GetById;
global using AlgoCode.Application.Features.Problem.Commands.CompileProblem;
global using AlgoCode.Application.Features.Problem.Commands.CreateProblem;
global using AlgoCode.Application.Features.Problem.Commands.DeleteProblem;
global using AlgoCode.Application.Features.Problem.Commands.SubmitProblem;
global using AlgoCode.Application.Features.Problem.Commands.UpdateProblem;
global using AlgoCode.Application.Features.Problem.Queries.GetAll;
global using AlgoCode.Application.Features.Problem.Queries.GetById;
global using AlgoCode.Application.Features.Problem.Queries.GetRandom;
global using AlgoCode.Application.Features.Sessions.Commands.ActivateSession;
global using AlgoCode.Application.Features.Sessions.Commands.CreateSession;
global using AlgoCode.Application.Features.Sessions.Commands.DeleteSession;
global using AlgoCode.Application.Features.Sessions.Commands.UpdateSession;
global using AlgoCode.Application.Features.Sessions.Queries.GetAll;
global using AlgoCode.Application.Features.Sessions.Queries.GetById;
global using AlgoCode.Application.Features.Solutions.Commands.DeleteSolution;
global using AlgoCode.Application.Features.Solutions.Commands.PostSolution;
global using AlgoCode.Application.Features.Solutions.Queries.GetAll;
global using AlgoCode.Application.Features.Solutions.Queries.GetById;
global using AlgoCode.Application.Features.Submissions.Queries.GetAll;
global using AlgoCode.Application.Features.Submissions.Queries.GetById;
global using AlgoCode.Application.Features.Subscriptions.Commands.CreateStripeSession;
global using AlgoCode.Application.Features.Subscriptions.Commands.CreateSubscription;
global using AlgoCode.Application.Features.Subscriptions.Commands.DeleteSubscription;
global using AlgoCode.Application.Features.Subscriptions.Commands.UpdateSubscription;
global using AlgoCode.Application.Features.Subscriptions.Commands.UpdateUserSubscription;
global using AlgoCode.Application.Features.Subscriptions.Queries.GetAll;
global using AlgoCode.Application.Features.Subscriptions.Queries.GetById;
global using AlgoCode.Application.Features.Subscriptions.Queries.GetUserSubscription;
global using AlgoCode.Application.Features.Tags.Commands.CreateTag;
global using AlgoCode.Application.Features.Tags.Commands.DeleteTag;
global using AlgoCode.Application.Features.Tags.Commands.UpdateTag;
global using AlgoCode.Application.Features.Tags.Queries.GetAll;
global using AlgoCode.Application.Features.Tags.Queries.GetById;
global using AlgoCode.Application.Features.TestCases.Commands.CreateTestCase;
global using AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase;
global using AlgoCode.Application.Features.TestCases.Queries.GetAll;
global using AlgoCode.Application.Features.TestCases.Queries.GetById;
global using AlgoCode.Application.Helpers;
global using AlgoCode.Domain.Constants;
global using AlgoCode.Domain.Entities.Identity;
global using AlgoCode.Infrastructure;
global using Mapster;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using Stripe.Checkout;
global using System.Security.Claims;
global using WebUI.Controllers.Base;
