$().ready(function () {
    $("#beneficiarioAlter").on('submit', function (event) {
        event.preventDefault();
        let cpf = $(this).find("#CPF").val();
        if (!validarCpf(cpf)) {
            swal({ title: "Atenção", text: "CPF Inválido", icon: "error", timer: 5000 });
            return;
        }
        $.ajax({
            method: 'post',
            url: urlAlteraBeneficiario,
            data: {
                "Nome": $(this).find("#Nome").val(),
                "CPF": desformataCpf(cpf),
                "Id": $(this).find("#Id").val(),
            },
            success: function (response) {
                swal({ title: "Sucesso", text: "Informações alteradas", icon: 'success' });
                loadBeneficiarios();
            },
            error: function (response) {
                swal({ title: "Erro", text: response, icon: 'error' });
            }
        });
    });
});

function editBeneficiario(id) {
    $("#beneficiarioInsert").hide('fast');
    $("#beneficiarioAlter").show('fast');
    $("#beneficiarioAlter").find("#Nome").val(beneficiarios[id].Nome);
    $("#beneficiarioAlter").find("#CPF").val(formataCpf(beneficiarios[id].CPF));
    $("#beneficiarioAlter").find("#Id").val(beneficiarios[id].Id);
}

function cancelEdit() {
    $("#beneficiarioInsert").toggle('fast');
    $("#beneficiarioAlter").toggle('fast');
    $("#beneficiarioAlter")[0].reset();
}

function deleteBeneficiario(id) {
    swal({ title: "Atenção", text: "Tem certeza que deseja excluir o beneficiário?", icon: "warning", dangerMode: true, buttons: true }).then(verify => {
        if (!verify) {
            swal.close();
            return;
        }
        $.ajax({
            url: urlDeleteBeneficiario,
            method: 'post',
            data: {
                'Id': beneficiarios[id].Id
            },
            success: function (response) {
                swal({ title: "Sucesso", text: "Beneficiario excluído.", icon: "success", timer: 2000 });
                loadBeneficiarios();
            },
            error: function (response) {
                swal({ title: "Erro", text: response, icon: "error" })
            }
        });
    })
}