
$(document).ready(function () {
    if (obj) {
        $.each(obj, function (key, value) {
            $('#formCadastro #' + key).val(value);
        });
    }
    $('#formCadastro input[name="CPF"]').mask("999.999.999-99");
    $('#formCadastro input[name="CEP"]').mask("99999-999");
    $('#formCadastro input[name="Telefone"]').mask("(99)99999-9999");

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        var cpf = $(this).find("#Cpf").val();
        if (!validarCpf(cpf)) {
            swal({ title: "Atenção", text: "CPF inválido", icon: 'warning', timer: 2000 });
            return;
        }
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Cpf": desformataCpf(cpf),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val()
            },
            error:
                function (r) {
                    if (r.status == 400)
                        swal({ title: "Erro", text: r.responseJSON, icon: "error", timer: 2000 });
                    else if (r.status == 500)
                        swal({ title: "Erro", text: "Ocorreu um erro interno de servidor", icon: "error", timer: 2000 });
                },
            success:
                function (r) {
                    swal({ title: "Sucesso", text: "Informações de cliente alteradas", icon: "success", timer: 2000 }).then(() => {
                        $("#formCadastro")[0].reset();
                        window.location.href = urlRetorno;
                    });
                }
        });
    })
})