﻿using System;
using System.Linq;
using InstaSharper.Classes;
using InstaSharper.Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace InstaSharper.Tests.Endpoints
{
    [Collection("Endpoints")]
    public class FeedTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _password = Environment.GetEnvironmentVariable("instaapiuserpassword");
        private readonly string _username = "alex_codegarage";

        public FeedTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [RunnableInDebugOnlyTheory]
        [InlineData("christmas")]
        public async void GetTagFeedTest(string tag)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var result = await apiInstance.GetTagFeedAsync(tag, 10);
            var tagFeed = result.Value;
            var anyMediaDuplicate = tagFeed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyStoryDuplicate = tagFeed.Stories.GroupBy(x => x.Id).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
            Assert.False(anyMediaDuplicate);
            Assert.False(anyStoryDuplicate);
        }


        [RunnableInDebugOnlyTheory]
        [InlineData("rock")]
        public async void GetUserTagFeedTest(string username)
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var result = await apiInstance.GetUserTagsAsync(username, 5);
            var tagFeed = result.Value;
            var anyMediaDuplicate = tagFeed.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            //assert
            Assert.True(result.Succeeded);
            Assert.NotNull(tagFeed);
            Assert.False(anyMediaDuplicate);
        }

        [RunnableInDebugOnlyFact]
        public async void GetFollowingRecentActivityFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            //no need to perform test if account marked as unsafe
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var getFeedResult = await apiInstance.GetFollowingRecentActivityAsync(5);
            var folloowingRecentFeed = getFeedResult.Value;

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(folloowingRecentFeed);
            Assert.True(!folloowingRecentFeed.IsOwnActivity);
        }

        [RunnableInDebugOnlyFact]
        public async void GetRecentActivityFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var getFeedResult = await apiInstance.GetRecentActivityAsync(5);
            var ownRecentFeed = getFeedResult.Value;
            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(ownRecentFeed);
            Assert.True(ownRecentFeed.IsOwnActivity);
        }

        [RunnableInDebugOnlyFact]
        public async void GetUserFeedTest()
        {
            //arrange
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserSessionData
                {
                    UserName = _username,
                    Password = _password
                });
            //act
            var loginSucceed = await apiInstance.LoginAsync();
            //no need to perform test if account marked as unsafe
            if (loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.LoginRequired
                || loginSucceed.Info.ResponseType == ResponseType.RequestsLimit)
            {
                _output.WriteLine("Unable to login: limit reached or checkpoint required");
                return;
            }
            Assert.True(loginSucceed.Succeeded);
            var getFeedResult = await apiInstance.GetUserTimelineFeedAsync(5);
            var feed = getFeedResult.Value;
            var anyDuplicate = feed.Medias.GroupBy(x => x.Code).Any(g => g.Count() > 1);
            var anyStoryDuplicate = feed.Stories.GroupBy(x => x.Id).Any(g => g.Count() > 1);

            //assert
            Assert.True(getFeedResult.Succeeded);
            Assert.NotNull(feed);
            Assert.False(anyDuplicate);
            Assert.False(anyStoryDuplicate);
        }
    }
}