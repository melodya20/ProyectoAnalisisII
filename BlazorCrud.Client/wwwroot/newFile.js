window.getUserAgentInfo = async () =>
{
    let sessionId = sessionStorage.getItem('sessionId');
    if (!sessionId) {
        sessionId = new Date().getTime().toString(); // Genera un sessionId único basado en la hora actual
        sessionStorage.setItem('sessionId', sessionId);
    }

    // Detección del navegador
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

    // Detección del sistema operativo
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

    // Detección de dispositivo
    let device = '';
    if (/Mobi/i.test(userAgent)) {
        device = "Móvil";
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
    }
}
