import { compare_strings_case_insensitive } from 'file://C:/Users/deschaseauxr/Documents/Donum/strings_compare.mjs'
import test_object from 'file://C:/Users/deschaseauxr/Documents/Donum/documents.json' assert { type: 'json' }
const truncate_date_to_day = date => JSON.stringify(date).substring(1, 11)
const output = test_object.value.map(({ dateDocument }) => truncate_date_to_day(dateDocument)).sort(compare_strings_case_insensitive).join('\n')
console.log(output)
process.exit(0)
