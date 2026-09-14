window.downloadOrOpenPdf = function (base64String, contentType, fileName, isInline) {
    // Converte a string Base64 para um Array de Bytes
    const byteCharacters = atob(base64String);
    const byteNumbers = new Array(byteCharacters.length);

    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: contentType || 'application/pdf' });
    const blobUrl = URL.createObjectURL(blob);

    if (isInline) {
        // ABRIR EM NOVA ABA (Inline)
        window.open(blobUrl, '_blank');
    } else {
        // DOWNLOAD DIRETO (Attachment)
        const link = document.createElement('a');
        link.href = blobUrl;
        link.download = fileName || 'termo.pdf';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    // Libera a memória da URL do blob após o uso
    setTimeout(() => URL.revokeObjectURL(blobUrl), 10000);
};

window.safeBackOrFallback = function (fallbackPath, blockedPaths) {
    const fallback = fallbackPath || '/portaria/controle-chave';
    const blocked = (blockedPaths || []).map(path => path.toLowerCase());

    if (!document.referrer) {
        window.location.href = fallback;
        return;
    }

    try {
        const previousUrl = new URL(document.referrer);
        const previousPath = previousUrl.pathname.toLowerCase().replace(/\/$/, '') || '/';
        const isInternal = previousUrl.origin === window.location.origin;
        const isBlocked = blocked.includes(previousPath);
        const isCurrentPage = previousUrl.href === window.location.href;

        if (isInternal && !isBlocked && !isCurrentPage) {
            window.location.href = previousUrl.href;
            return;
        }
    } catch {
        // Se a origem não puder ser validada, utiliza o destino seguro.
    }

    window.location.href = fallback;
};
