import * as fs from 'fs/promises'
function get_random_date() {
  const time_min = new Date(1990, 0, 1).getTime()
  const time_max = new Date(2022, 10, 20).getTime()
  const time_interval = time_max - time_min
  const random_value = time_min + Math.trunc(Math.random() * time_interval)
  return new Date(JSON.stringify(new Date(random_value)).substring(1, 11))
}
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
json_object.value = json_object.value.map(doc => {
  return {
    dateDocument: get_random_date(),
    deposeLe: get_random_date(),
    ...doc,
  }
})
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')
