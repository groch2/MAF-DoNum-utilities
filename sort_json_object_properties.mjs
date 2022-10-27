import test_object from 'file://C:/Users/deschaseauxr/Documents/Donum/documents.json' assert { type: 'json' }
import { writeFile } from 'fs/promises'
test_object.value = test_object.value.map(obj => Object.fromEntries(Object.keys(obj).sort().map(key => [key, obj[key]])))
await writeFile('C:/Users/deschaseauxr/Documents/Donum/documents.json', JSON.stringify(test_object, null, 2))
process.exit(0)
