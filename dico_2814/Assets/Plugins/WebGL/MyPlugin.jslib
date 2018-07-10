var MyPlugin = {
    SendScore: function (score) {
    	gameManagement.sendScore(score);
    },

    GetConfig: function () {
    	var returnStr = gameManagement.getConfig()
	    var buffer = _malloc(returnStr.length + 1);
	    writeStringToMemory(returnStr, buffer);
	    return buffer;
  	},
};
mergeInto(LibraryManager.library, MyPlugin);