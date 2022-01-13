

function Falha() {
    Swal.fire(
        'Falha',
        'Favor verificar todos os campos',
        'error'
    );
}

function Sucesso(data) {

    if (data.msg) {
        Swal.fire(
            'Sucesso',
            data.msg,
            'success'
        );
    }

    if (data.repetido) {
        Swal.fire(
            'Essa Tecnologia já está cadastrada',
            data.repetido,
            'error'
        );
    }

    if (data.vazio) {
        Swal.fire(
            'Algum campo obrigatório está vazio',
            data.vazio,
            'error'
        );
    }

    if (data.removido) {
        
        Swal.fire(
            'Skill removida com sucesso',
            data.removido,
            'success'       
        ).then(function () {      
            window.location = "Skills";
        });
    }

    if (data.skilleditada) {

        Swal.fire(
            'Sucesso!',
            data.skilleditada,
            'success'
        ).then(function () {
            window.location = "Skills";
        });
    }

    
    if (data.userremovido) {

        Swal.fire(
            'Sucesso!',
            data.userremovido,
            'success'
        ).then(function () {
            window.location = "Usuarios";
        });
    }

    if (data.usereditado) {

        Swal.fire(
            'Sucesso!',
            data.usereditado,
            'success'
        ).then(function () {
            window.location = "Usuarios";
        });
    }

    if (data.funcionarioremovido) {

        Swal.fire(
            'Sucesso!',
            data.funcionarioremovido,
            'success'
        ).then(function () {
            window.location = "Funcionarios";
        });
    }

    if (data.funcionarioeditado) {
        Swal.fire(
            'Sucesso!',
            data.funcionarioeditado,
            'success'
        ).then(function () {
            window.location = "Funcionarios";
        });
    }

    if (data.semacesso) {
        Swal.fire(
            'Erro!',
            data.semacesso,
            'error'
        ).then(function () {
            window.location = "Skills";
        });
    }

    if (data.naologado) {

        Swal.fire(
            'Erro',
            data.naologado,
            'error'
        );
    }

    if (data.funcrepetido) {
        Swal.fire(
            'Erro',
            data.funcrepetido,
            'error'
        );
    }

    if (data.favorlogar) {

        Swal.fire(
            'Olá!',
            data.favorlogar,
            'success'

        );
    }

    


    if (data.logado) {
        Swal.fire(
            'Sucesso!',
            data.logado,
            'success'
        ).then(function () {
            window.location = "Home";
        });
    }

    if (data.cadastrofuncionarioskill) {
        Swal.fire(
            'Sucesso!',
            data.cadastrofuncionarioskill,
            'success'
        ).then(function () {
            window.location = "Funcionarios";
        });
    }

    if (data.removidoSkillFuncionario, data.teste) {
        Swal.fire(
            'Sucesso!',
            data.removidoSkillFuncionario,
            'success'
        ).then(function () {
            window.location = "SkillsFuncionarios?id="+data.teste;
        });
    }

    
 
}