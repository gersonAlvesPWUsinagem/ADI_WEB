window.agenteLocal = {
    obterDadosMaquina: async function () {
        try {
            // O fetch roda no navegador do cliente e acha o localhost/agente dele
            const response = await fetch('http://127.0.0.1:5005/api/maquina');
            if (response.ok) {
                const dados = await response.json();
                return JSON.stringify(dados);
            }
            return null;
        } catch (error) {
            console.error("Agente local inacessível no cliente:", error);
            return null;
        }
    }
};