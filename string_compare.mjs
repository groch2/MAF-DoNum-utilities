const are_strings_equals_case_insensitive = (a, b) =>
  a?.localeCompare(b, undefined, { sensitivity: 'accent' }) === 0
export default are_strings_equals_case_insensitive
// are_strings_equals_case_insensitive