﻿CREATE PROC FI_SP_VerificaBeneficiario
	@CPF VARCHAR(11)
AS
BEGIN
		SELECT CPF FROM BENEFICIARIOS WITH(NOLOCK) WHERE CPF LIKE '%'+@CPF+'%'
END