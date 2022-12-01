const obterTarefas = async () => {
    const url = '/api/tarefas';
    const resposta = await fetch(url); 
    
    if (!resposta.ok)
    {
        alert('Erro ao obter lista.');
        return;
    }

    const lista = document.getElementById('lista-tarefas');
    lista.innerHTML = '';

    const resultado = await resposta.json();

    for (let tarefa of resultado) {
        lista.insertAdjacentHTML('beforeend', `<li>${tarefa.id} - ${tarefa.descricao} (${tarefa.concluida})`);
    }
};

const buscaPorId = async (ev) => {
    ev.preventDefault();

    const campoTarefa = document.getElementById('tarefa-indicada');
    campoTarefa.innerHTML = '';

    const campoId = document.getElementById('id-tarefa');
    const url = `/api/tarefas/${campoId.value}`;

    const resposta = await fetch(url); 
    
    if (!resposta.ok)
    {
        campoTarefa.innerHTML = 'Erro ao obter tarefa.';
        return;
    }

    const resultado = await resposta.json();

    campoTarefa.innerHTML = `A tarefa número ${resultado.id} é: ${resultado.descricao}`;
};


document.addEventListener('DOMContentLoaded', () => {
    // PRENCHER A LISTA COMPLETA
    obterTarefas();

    // ADICIONAR EVENTO NO BOTÃO
    const botaoBusca = document.getElementById('buscar-id');
    botaoBusca.addEventListener('click', buscaPorId);
});