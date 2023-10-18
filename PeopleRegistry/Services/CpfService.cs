using PeopleRegistry.Repositories;
using PeopleRegistry.Repositories.Interfaces;

namespace PeopleRegistry.Services
{
    public class CpfService
    {
		private readonly IPersonRepository _personRepository;

        public CpfService(IPersonRepository personRepository)
        {
			_personRepository = personRepository;
        }

		public bool VerificaCpfBanco(string cpf)
		{
			if (_personRepository.GetPersonByCpf(cpf) != null)
				return false;
			return true;
		}

		public bool IsValidCpf(string cpf)
		{
			int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
				return false;
			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for (int i = 0; i < 9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();
			if (!cpf.EndsWith(digito))
				return false;
			return true;
		}
	}
}