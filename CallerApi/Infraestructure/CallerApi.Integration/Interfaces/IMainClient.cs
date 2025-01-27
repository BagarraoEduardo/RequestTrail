using System;

namespace CallerApi.Integration.Interfaces;

public interface IMainClient
{
    Task SuccessExample();
    Task BadRequestExample();
    Task InternalServerErrorExample();
}
