

    function TCXCSFunction()
    {
        //======== 1. Initailize Vairables ============
        this.AsseblyName = "";
        this.TypeName = "";
        this.FunctionName = "";
        this._paramString = "";
        this._split0 = "#;#";
        this._split1 = "#:#";
        
        //======== Function: Add Parameter ============
        this.AddParameter = function( type, value )
        {
            var exps = type + this._split1 + value + this._split0;
            this._paramString += exps;
        }
        
        //======= Function: Execute ============
        this.Execute = function()
        {
            //============ 1. Verify parameter missing ===========
            if( this.AsseblyName == "" )
            {
                window.alert( "(tcs_cs_function_calling.js)Parameter Missing" );
                return false;
            }
            if( this.TypeName == "" )
            {
                window.alert( "(tcs_cs_function_calling.js)Class name missing" );
                return false;
            }
            if( this.FunctionName == "" )
            {
                window.alert( "(tcs_cs_function_calling.js)Method or function name missing" );
                return false;
            }
            
            //========== 2. Combine out the calling name and parameters ===============
            var param = "";
            param += "asmb" + this._split1 + this.AsseblyName + this._split0;
            param += "type" + this._split1 + this.TypeName + this._split0;
            param += "func" + this._split1 + this.FunctionName + this._split0;
            param += this._paramString;
            param = escape( param );
            
            //======= 3. Calling XMLHTTP Interface ============
            var xhttp= new ActiveXObject("Microsoft.XMLHTTP");
            try
            {
                xhttp.open("GET", "../../framework/mechanism/exec_cs_function/ExecCSFunction.aspx?param="+param, false);
                xhttp.send();
                var vsRetval = xhttp.responseText;
                xhttp = null;
                if( vsRetval.substring( 0, 2 ) == "/1" )    
                { 
                    window.alert( vsRetval.substring( 2 ) ); 
                    return false; 
                }
                return vsRetval;
            }
            catch(error)
            {
                window.alert( "(tcs_cs_function_calling.js)CSFunction Process failed:" + error.description );
                return false;
            }    
        }
    }
