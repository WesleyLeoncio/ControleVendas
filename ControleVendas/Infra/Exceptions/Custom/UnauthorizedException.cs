namespace ControleVendas.Infra.Exceptions.custom;

public class UnauthorizedException(string msg) : Exception(msg);