//
//  ISNCamera.h
//  Unity-iPhone
//
//  Created by Osipov Stanislav on 6/10/14.
//
//

#import <Foundation/Foundation.h>
#include "ISNDataConvertor.h"

@interface ISNCamera : NSObject

+ (id)   sharedInstance;
- (void) saveToCameraRoll:(NSString*)media;

@end
