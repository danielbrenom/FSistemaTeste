let beneficiarios;
$().ready(function () {
	$("#beneficiarioAlter").toggle('fast');
	$("#beneficiarioModal").on('show.bs.modal', function (modal) {
		$(this).find("#ClientId").val(obj.Id);
		$(this).find('input[name="CPF"]').each(function (key, element) {
			$(element).mask("999.999.999-99");
		});
		loadBeneficiarios();
	});

	$("#beneficiarioInsert").on('submit', function (e) {
		e.preventDefault();
		let cpf = $(this).find("#CPF").val();
		if (!validarCpf(cpf)) {
			swal({ title: "Atenção", text: "CPF Inválido", icon: "error", timer: 5000 });
			return;
		}
		$.ajax({
			url: urlInsertBeneficiario,
			method: 'post',
			data: {
				"CPF": desformataCpf(cpf),
				"Nome": $(this).find("#Nome").val(),
				"IdCliente": $(this).find("#ClientId").val()
			}, success: function (response) {
				swal({ title: "Sucesso", text: "Beneficiario cadastrado", icon: 'success', timer: 2000 });
				loadBeneficiarios();
			},
			error: function (response) {
				swal({ title: "Erro", text: response.responseJSON, icon: 'error' });
			}
		});
	});
});

function loadBeneficiarios() {
	$("#tableBody").empty();
	$.post(urlListBeneficiario, {
		"IdCliente": obj.Id
	}, function (response) {
		if (response) {
			beneficiarios = response;
			$.each(response, function (key, object) {
				var row = '<tr>' +
					'<td>' + object.Nome + '</td>' +
					'<td>' + formataCpf(object.CPF) + '</td>' +
					'<td><button class="btn btn-info" onclick="editBeneficiario(' + key + ')">Alterar</button> ' +
					'<button class="btn btn-danger" onclick="deleteBeneficiario(' + key + ')"> Excluir</button ></td> ' +
					'</tr>';
				$("#tableBody").append(row);
			});
		} else {
			window.alert("Ocorreu um erro");
		}
	});
}

