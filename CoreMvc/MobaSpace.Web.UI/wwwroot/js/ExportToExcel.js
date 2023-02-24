
function TableSortieTOExcel(id, name, type, fun, dl) {
    var elt = document.getElementById(id);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fun || ('resident-' + name + '-Enregistrement-Sortie.' + (type || 'xlsx')));
}
