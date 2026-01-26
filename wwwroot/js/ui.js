/*
파일명   : js/ui.js
기능명   : 화면 갱신용 
상세     : 사용자가 파일 목록을 가져오거나 파일을 업로드 했을 때, 조회 목록을 갱신한다.
*/

/******************************
사용자 화면 갱신
*******************************/
export async function updateFileTable(files) {
  const tbody = document.getElementById('tby-file');
  tbody.innerHTML = files.map(file => `
  <tr>
    <td class="td-fileno">${file.fileNo}</td>
    <td>${file.fileKind}</td>
    <td class="td-filenm">${file.fileNm}</td>
    <td>${file.fileSize}</td>
    <td>${file.entryDt}</td>
  </tr>
  `).join('');
  
  //파일명 CSS 추가 함수 호출
  addFileNameCss();
}

/******************************
 파일명 css 추가
 *******************************/
export async function addFileNameCss() {
  const fileNameCells = document.querySelectorAll('.td-filenm');
  if (fileNameCells) {
    fileNameCells.forEach(cell => {
      cell.style.color = 'blue';
      cell.style.fontWeight = 'bold';
      cell.style.textDecoration = 'underline';
      cell.style.cursor = 'pointer';
    });
  }
}