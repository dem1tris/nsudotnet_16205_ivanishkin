using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace task7_social {
    internal class VkApiWrapper {
        private static long _myUserId = 32101335;
        private readonly VkApi _vk;
        private readonly Lazy<Random> _lazyRandom;

        public VkApiWrapper() {
            var services = new ServiceCollection();
            services.AddAudioBypass();
            _vk = new VkApi(services);
            _lazyRandom = new Lazy<Random>();
        }

        public bool Authorize(string token) {
            //long? captchaSid = null;
            //string captchaKey = null;
            //bool retry = false;
            //do {
            try {
                _vk.Authorize(new ApiAuthParams {
                    AccessToken = token,
                    Settings = Settings.All,
                    //TwoFactorAuthorization = () => {
                    //    Console.WriteLine("Enter SMS-code:");
                    //    return Console.ReadLine();
                    //},
                    //CaptchaSid = captchaSid,
                    //CaptchaKey = captchaKey
                });
            }
            catch (VkNet.Exception.VkAuthorizationException) {
                return false;
            }
            //catch (VkNet.Exception.CaptchaNeededException e)
            //{
            //    captchaSid = e.Sid;
            //    Console.WriteLine($"Need to solve the captcha: {e.Img}");
            //    Console.WriteLine("Enter the answer:");
            //    captchaKey = Console.ReadLine();
            //    retry = true;
            //}
            //} while (retry);

            return true;
        }

        public List<string> GetFriendList() {
            var friends = _vk.Friends.Get(new FriendsGetParams
                {UserId = _myUserId, Fields = ProfileFields.FirstName | ProfileFields.LastName});

            List<string> friendsStrings = new List<string>(friends.Count);
            foreach (var friend in friends) {
                friendsStrings.Add($"{friend.FirstName} {friend.LastName}");
            }

            return friendsStrings;
        }

        public void SendMessage(string message) {
            var @params = new MessagesSendParams {

                RandomId = _lazyRandom.Value.Next(),
                Message = message,
                UserId = _myUserId
            };
            _vk.Messages.Send(@params);
        }
    }
}