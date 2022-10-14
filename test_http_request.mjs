import { request } from 'https';

//The url we want is: 'www.random.org/integers/?num=1&min=1&max=10&col=1&base=10&format=plain&rnd=new'
const options = {
  host: 'www.random.org',
  path: '/integers/?num=1&min=1&max=10&col=1&base=10&format=plain&rnd=new'
};

const callback = function (response) {
  let str = '';

  //another chunk of data has been received, so append it to `str`
  response.on('data', function (chunk) {
    str += chunk;
    console.log('http request data chunk')
  });

  //the whole response has been received, so we just print it out here
  response.on('end', function () {
    console.log(str);
  });
  console.log('http request callback')
}

request(options, callback).end();
console.log('terminé')