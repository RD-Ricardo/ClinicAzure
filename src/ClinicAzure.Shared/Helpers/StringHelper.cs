namespace ClinicAzure.Shared.Helpers
{
    public static class StringHelper
    {
        public static bool ValidateCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !long.TryParse(cpf, out _))
                return false;
            int[] weights = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += (cpf[i] - '0') * weights[i];
            }
            int remainder = sum % 11;
            int firstDigit = remainder < 2 ? 0 : 11 - remainder;
            if (firstDigit != (cpf[9] - '0'))
                return false;
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (cpf[i] - '0') * (weights[i] + 1);
            }
            remainder = sum % 11;
            int secondDigit = remainder < 2 ? 0 : 11 - remainder;
            return secondDigit == (cpf[10] - '0');
        }

        public static bool ValidateCnpj(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14 || !long.TryParse(cnpj, out _))
                return false;
            int[] weights1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weights2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum1 = 0;
            for (int i = 0; i < 12; i++)
            {
                sum1 += (cnpj[i] - '0') * weights1[i];
            }
            int remainder1 = sum1 % 11;
            int firstDigit = remainder1 < 2 ? 0 : 11 - remainder1;
            if (firstDigit != (cnpj[12] - '0'))
                return false;
            int sum2 = 0;
            for (int i = 0; i < 13; i++)
            {
                sum2 += (cnpj[i] - '0') * weights2[i];
            }
            int remainder2 = sum2 % 11;
            int secondDigit = remainder2 < 2 ? 0 : 11 - remainder2;
            return secondDigit == (cnpj[13] - '0');
        }
    }
}
