const char_code_A = 'A'.charCodeAt(0)
const alphabet = Object.keys(new Array(26).fill(0)).map(v => parseInt(v)).map(v => String.fromCharCode(char_code_A + v))
const get_random_word = (length) => new Array(length).fill(0).map(_ => alphabet[Math.trunc(Math.random() * 26)]).join("")
export default get_random_word
