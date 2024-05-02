window.GeneratePDF = async (qrCodeInfos, qrCodeImageUrls, fileName) => {
    try {
        const doc = new jsPDF();
        const pageWidth = doc.internal.pageSize.width;
        const pageHeight = doc.internal.pageSize.height;
        const qrCodeSize = 50; // Ajusta este valor según el tamaño deseado para los códigos QR
        const margin = 20; // Ajusta este valor según el margen deseado
        const qrCodesPerRow = Math.floor((pageWidth - 2 * margin) / qrCodeSize);
        const qrCodesPerColumn = Math.floor((pageHeight - 2 * margin) / qrCodeSize);

        let x = margin;
        let y = margin;

        for (let i = 0; i < qrCodeInfos.length; i++) {
            doc.setFontSize(12);
            doc.setFont("courier");
            doc.setFontType("normal");

            // Se asigna la URL de la imagen
            var img = new Image();
            img.src = qrCodeImageUrls[i];
            await new Promise((resolve) => {
                img.onload = function () {
                    doc.addImage(this, x, y, qrCodeSize, qrCodeSize);
                    resolve();
                }
            });

            // Añade el texto debajo del código QR
            doc.text(x, y + qrCodeSize + 10, qrCodeInfos[i]);

            // Actualiza las coordenadas x e y para el siguiente código QR
            x += qrCodeSize;
            if ((i + 1) % qrCodesPerRow == 0) {
                x = margin;
                y += qrCodeSize + 20; // Añade espacio extra para el texto
                if ((i + 1) % (qrCodesPerRow * qrCodesPerColumn) == 0 && i < qrCodeInfos.length - 1) {
                    y = margin;
                    doc.addPage();
                }
            }

            // Verifica si hay espacio para más códigos QR en la página actual
            if (y + qrCodeSize + 20 > pageHeight || x + qrCodeSize > pageWidth) {
                x = margin;
                y = margin;
                doc.addPage();
            }
        }

        doc.save(fileName + ".pdf");
    } catch (error) {
        console.error("Error generating PDF: ", error);
    }
}