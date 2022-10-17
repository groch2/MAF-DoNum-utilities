import are_strings_equals_case_insensitive from './string_compare.mjs'
import document from 'file://C:/Users/deschaseauxr/Documents/Donum/document.json' assert { type: 'json' }
import duplicate_document from 'file://C:/Users/deschaseauxr/Documents/DONUM/duplicate_properties.json'assert { type: 'json' }

// console.log({ document_properties, duplicated_properties })
const get_alphabetically_sorted_object_properties = json_object =>
  [...Object.keys(json_object)].sort((a, b) => are_strings_equals_case_insensitive(a, b))
const document_properties = get_alphabetically_sorted_object_properties(document)
const duplicate_document_properties = get_alphabetically_sorted_object_properties(duplicate_document)
console.log({ document_properties, duplicate_document_properties })