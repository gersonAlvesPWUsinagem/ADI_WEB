export function setCookie(name, value, maxAgeSeconds, sameSite, secure, path) {
    const encodedName = encodeURIComponent(name);
    const encodedValue = encodeURIComponent(value);
    const cookiePath = path || "/";
    let cookie = `${encodedName}=${encodedValue}; path=${cookiePath}; max-age=${maxAgeSeconds}; SameSite=${sameSite || "Strict"}`;

    if (secure) {
        cookie += "; Secure";
    }

    document.cookie = cookie;
}

export function getCookie(name) {
    const encodedName = `${encodeURIComponent(name)}=`;
    const cookies = document.cookie.split("; ");
    const cookie = cookies.find(item => item.startsWith(encodedName));

    if (!cookie) {
        return null;
    }

    return decodeURIComponent(cookie.substring(encodedName.length));
}

export function removeCookie(name, sameSite, secure, path) {
    const encodedName = encodeURIComponent(name);
    const cookiePath = path || "/";
    let cookie = `${encodedName}=; path=${cookiePath}; max-age=0; SameSite=${sameSite || "Strict"}`;

    if (secure) {
        cookie += "; Secure";
    }

    document.cookie = cookie;
}
