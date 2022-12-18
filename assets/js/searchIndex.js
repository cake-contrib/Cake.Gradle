
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"WrapperFactory",
            content:"WrapperFactory",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/WrapperFactory',
            title:"WrapperFactory",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"JsonMapper",
            content:"JsonMapper",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonMapper',
            title:"JsonMapper",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"JsonType",
            content:"JsonType",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonType',
            title:"JsonType",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"IZipAdapter",
            content:"IZipAdapter",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle.Abstractions/IZipAdapter',
            title:"IZipAdapter",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"IWebAdapter",
            content:"IWebAdapter",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle.Abstractions/IWebAdapter',
            title:"IWebAdapter",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"GradleLogLevel",
            content:"GradleLogLevel",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle/GradleLogLevel',
            title:"GradleLogLevel",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"JsonWriter",
            content:"JsonWriter",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonWriter',
            title:"JsonWriter",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"GradleRunnerSettings",
            content:"GradleRunnerSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle/GradleRunnerSettings',
            title:"GradleRunnerSettings",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"GradleBootstrapper",
            content:"GradleBootstrapper",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle.Bootstrap/GradleBootstrapper',
            title:"GradleBootstrapper",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"ImporterFunc",
            content:"ImporterFunc",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/ImporterFunc_2',
            title:"ImporterFunc<TJson, TValue>",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"IJsonWrapper",
            content:"IJsonWrapper",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/IJsonWrapper',
            title:"IJsonWrapper",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"JsonReader",
            content:"JsonReader",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonReader',
            title:"JsonReader",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"GradleRunner",
            content:"GradleRunner",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle/GradleRunner',
            title:"GradleRunner",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"ExporterFunc",
            content:"ExporterFunc",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/ExporterFunc_1',
            title:"ExporterFunc<T>",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"GradleVersionIdentifier",
            content:"GradleVersionIdentifier",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle.Bootstrap/GradleVersionIdentifier',
            title:"GradleVersionIdentifier",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"GradleBootstrapAlias",
            content:"GradleBootstrapAlias",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle/GradleBootstrapAlias',
            title:"GradleBootstrapAlias",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"JsonMockWrapper",
            content:"JsonMockWrapper",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonMockWrapper',
            title:"JsonMockWrapper",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"JsonData",
            content:"JsonData",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonData',
            title:"JsonData",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"GradleRunnerAlias",
            content:"GradleRunnerAlias",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle/GradleRunnerAlias',
            title:"GradleRunnerAlias",
            description:""
        }
    );
    a(
        {
            id:19,
            title:"JsonToken",
            content:"JsonToken",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonToken',
            title:"JsonToken",
            description:""
        }
    );
    a(
        {
            id:20,
            title:"GradleDownloadInformation",
            content:"GradleDownloadInformation",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/Cake.Gradle.Bootstrap/GradleDownloadInformation',
            title:"GradleDownloadInformation",
            description:""
        }
    );
    a(
        {
            id:21,
            title:"JsonException",
            content:"JsonException",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Gradle/api/LitJson/JsonException',
            title:"JsonException",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
