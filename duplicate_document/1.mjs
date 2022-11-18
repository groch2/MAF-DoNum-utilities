import * as fs from 'fs/promises'
import document_origin from './document_origin.json' assert { type: 'json' }
import duplicate_request from './duplicate_request.json' assert { type: 'json' }

for (const prop in duplicate_request) {
  if (document_origin[prop] !== undefined) {
    duplicate_request[prop] = document_origin[prop]
  } else {
    console.debug(prop)
  }
}

const duplicate_request_sorted = Object.fromEntries(Object.keys(duplicate_request).sort().map(key => [key, duplicate_request[key]]))

const stringified_duplicate_request = JSON.stringify(duplicate_request_sorted, undefined, 2)
const file_path = './duplicate_request_updated.json'
await fs.writeFile(file_path, stringified_duplicate_request, { encoding: 'utf8' })