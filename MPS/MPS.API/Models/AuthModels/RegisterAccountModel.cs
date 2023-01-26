using MPS.DAL.Models.Tools;

namespace MPS.API.Models.AuthModels;

public record RegisterAccountModel(string Login, Role Role, string Password);