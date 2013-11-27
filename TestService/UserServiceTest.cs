using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using E_BusinessService;
using E_BusinessModel;
using E_BusinessCommon;

namespace TestService
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService _userService;

       public  UserServiceTest()
        {
            _userService = new UserService();
        }

        [TestMethod]
        public void User()
        {
            BusinessUser user = new BusinessUser()
            {
                Id = "3867b6d4-8c84-45fc-a43b-ddc8e67bed48",
                 BusinessRoleId = "1CB7A8B4-ECEF-4927-8C57-0592122FAD2E",
                   CreateTime=DateTime.Now,
                    UserName="randy",
                    UserPassWord="wing"

            };
            //var u = _userService.CreateUser(user); // OK
            //_userService.UpdateUser(user);        //  OK
            //_userService.RemoveUser(user);         // OK
            //var s= _userService.RemoveUser(x => x.Id == "3867b6d4-8c84-45fc-a43b-ddc8e67bed48" && x.UserName == "randy"); //OK
            //var uu = _userService.GetUserInfo("3867b6d4-8c84-45fc-a43b-ddc8e67bed48"); //OK
            var u = _userService.GetUserList(x => x.UserIDCard == 111111111, s => s.CreateTime, true); //OK
            //var page = _userService.GetUserList(null, null, false, 1, 2);  //OK
            //Assert.IsTrue(u.Id == user.Id);
        }
    }
}
