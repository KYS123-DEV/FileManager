/*
파일명   : js/app.js
기능명   : 엔트리 포인트 - 진입점
상세     : 사용자가 버튼을 눌렀을 때, 어떤 순서로 동작할지 지시
*/

import { getFiles } from './api.js';
import { updateFileTable } from './ui.js';

/******************************
서버에서 파일 목록 가져오기
*******************************/
async function init() {
  const files = await getFiles();
  updateFileTable(files);
}

init();