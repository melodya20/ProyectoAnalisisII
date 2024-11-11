// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });


window.getUserAgentInfo = async () => {
    // Obtener o generar sessionId
    let sessionId = sessionStorage.getItem('sessionId');
    if (!sessionId) {
        sessionId = new Date().getTime().toString(); // Genera un sessionId �nico basado en la hora actual
        sessionStorage.setItem('sessionId', sessionId);
    }

    // Detecci�n del navegador
    let userAgent = navigator.userAgent;
    let browser = '';
    if (userAgent.indexOf("Edg") > -1) {
        browser = "Edge";
    } else if (userAgent.indexOf("Chrome") > -1) {
        browser = "Chrome";
    } else if (userAgent.indexOf("Firefox") > -1) {
        browser = "Firefox";
    } else if (userAgent.indexOf("Safari") > -1 && userAgent.indexOf("Chrome") === -1) {
        browser = "Safari";
    } else {
        browser = "Otro navegador";
    }

    // Detecci�n del sistema operativo
    let os = '';
    if (userAgent.indexOf("Win") !== -1) {
        os = "Windows";
    } else if (userAgent.indexOf("Mac") !== -1) {
        os = "macOS";
    } else if (userAgent.indexOf("Linux") !== -1) {
        os = "Linux";
    } else {
        os = "Otro sistema operativo";
    }

    // Detecci�n de dispositivo
    let device = '';
    if (/Mobi/i.test(userAgent)) {
        device = "M�vil";
    } else if (/Tablet/i.test(userAgent)) {
        device = "Tablet";
    } else {
        device = "Desktop"; // Incluye laptops en "Desktop"
    }

    return {
        userAgent: navigator.userAgent,
        ipAddress: await fetch('https://api.ipify.org?format=json').then(res => res.json()).then(data => data.ip),
        os: os, // Sistema operativo detectado
        device: device, // Tipo de dispositivo detectado
        browser: browser, // Navegador detectado
        sessionId: sessionId // SessionId generado o recuperado
    };
};

