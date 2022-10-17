import { compare_strings_case_insensitive, are_strings_equals_case_insensitive } from './strings_compare.mjs'
import document from 'file://C:/Users/deschaseauxr/Documents/Donum/document.json' assert { type: 'json' }
import duplicate_document from 'file://C:/Users/deschaseauxr/Documents/DONUM/duplicate_properties.json'assert { type: 'json' }

// console.log({ document_properties, duplicated_properties })
const get_object_properties = json_object => [...Object.keys(json_object)]
const document_properties = get_object_properties(document)
const duplicate_document_properties = get_object_properties(duplicate_document)
// console.log({ document_properties, duplicate_document_properties })
const document_properties_missing_from_duplication =
  document_properties.filter(
    doc_prop => duplicate_document_properties.findIndex(dupl_prop => are_strings_equals_case_insensitive(doc_prop, dupl_prop)) === -1)
    .sort((a, b) => compare_strings_case_insensitive(a, b))
console.log({ document_properties_missing_from_duplication })
