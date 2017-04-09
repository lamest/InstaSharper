﻿using System;
using System.Linq;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class FollowersTest
    {
        private readonly ITestOutputHelper _output;

        public FollowersTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("therock")]
        public async void GetUserFollowersTest(string username)
        {
            var currentUsername = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = currentUsername,
                Password = password
            });
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetUserFollowersAsync(username, 10);
            var followers = result.Value;
            var anyDuplicate = followers.GroupBy(x => x.Pk).Any(g => g.Count() > 1);

            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
            Assert.False(anyDuplicate);
        }

        [RunnableInDebugOnlyFact]
        public async void GetCurrentUserFollwersTest()
        {
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = username,
                Password = password
            });
            if (!TestHelpers.Login(apiInstance, _output)) return;
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.GetCurrentUserFollowersAsync();
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }

        [RunnableInDebugOnlyTheory]
        [InlineData(196754384)]
        public async void FollowUserTest(long userId)
        {
            var currentUsername = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
            {
                UserName = currentUsername,
                Password = password
            });
            if (!TestHelpers.Login(apiInstance, _output)) return;
            var result = await apiInstance.FollowUserAsync(userId);
            var followers = result.Value;
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(followers);
        }
    }
}