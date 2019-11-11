using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Application.Commands.OperateCommands.UserCommands;

namespace User.API.Application.Validations.UserValide
{
    public class UserCreateValide : AbstractValidator<CreateUserCommand>
    {

        public UserCreateValide(ILogger<UserCreateValide> logger)
        {
            RuleFor(command => command.UserId).NotEmpty()
                .WithMessage("用户登录账号不能为空")
                  .Matches("[a-zA-Z]")
                  .WithMessage("登录账号必须为英文，不可以包含特殊符号")
                  ;

            RuleFor(command => command.UserName).NotEmpty()
                  .WithMessage("用户姓名不能为空")
                  .Matches("[\u4e00-\u9fa5]")
                  .WithMessage("用户姓名必须为中文")
              ;
            RuleFor(command => command.UserPwd).NotEmpty()
                    .WithMessage("用户密码不能为空")
                   .Length(6, 12)
                    .WithMessage("密码必须长度必须在6到12位之间");

            RuleFor(command => command.PhoneCall)
                  .Must(phoneCall => phoneCall.Length == 11)
                  .WithMessage("手机号码长度必须为11位");

            RuleFor(command => command.Gender).Must(a=>a.Equals("男")||a.Equals("女"))
                .WithMessage("性别必须为\"男\"或者\"女\"")
                ;
            RuleFor(command => command.Email).NotEmpty()
                   .WithMessage("邮箱不能为空")
                   .EmailAddress()
                   .When(m => !string.IsNullOrWhiteSpace(m.Email)).WithMessage("邮箱格式不正确");
            RuleFor(command => command.CreateUserId).NotEmpty()
               .WithMessage("用户登录账号不能为空")
                 .Matches("[a-zA-Z]")
                 .WithMessage("登录账号必须为英文，不可以包含特殊符号")
                 ;
            RuleFor(command => command.CreateUserName).NotEmpty()
                  .WithMessage("用户姓名不能为空")
                  .Matches("[\u4e00-\u9fa5]")
                  .WithMessage("用户姓名必须为中文")
                  ;
            RuleFor(command => command.UpdateTime).NotEmpty()
                  .WithMessage("更新时间不能为空")
                 ;
            RuleFor(command => command.Createtime).NotEmpty()
                 .WithMessage("创建时间不能为空")
                ;
            logger.LogTrace("----- 命令实例创建 - {ClassName}", GetType().Name);
        }

    }
}
